FROM mcr.microsoft.com/dotnet/sdk:6.0 AS sdk

EXPOSE 5500 5501
ENV ASPNETCORE_URLS=http://+:5500;https://+:5501

RUN useradd --create-home --shell /bin/bash ersmsuser
USER ersmsuser

WORKDIR /ersms/Backend

ENV PATH="$PATH:/home/ersmsuser/.dotnet/tools"
RUN dotnet tool install --global dotnet-ef

ADD ./Backend /ersms/Backend
RUN dotnet restore

RUN dotnet dev-certs https --export-path $HOME/.aspnet/https/ersms.pem --format Pem --no-password

HEALTHCHECK --interval=5s --timeout=10s --retries=10 CMD curl --fail http://localhost:5500/healthcheck || exit 1

CMD ["/bin/bash", "-c", "([ '$(ls -A /ersms/Backend/Data/Migrations)' ] && dotnet ef migrations add InitialMigration -o Data/Migrations || echo '') && dotnet ef database update && dotnet watch run"]
