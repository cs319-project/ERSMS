
FROM node:16.15-alpine as builder

COPY ./Frontend/package.json ./Frontend/package-lock.json ./

RUN npm ci && mkdir /ersms && mkdir /ersms/Frontend && mv ./node_modules ./ersms/Frontend

WORKDIR /ersms/Frontend

COPY ./Frontend /ersms/Frontend/

RUN npm run build

FROM nginx:alpine

COPY --from=builder /ersms/Frontend/dist/Frontend /usr/share/nginx/html

EXPOSE 8000
