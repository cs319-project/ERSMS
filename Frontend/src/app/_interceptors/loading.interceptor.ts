import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { identity, Observable } from 'rxjs';
import { delay, finalize } from 'rxjs/operators';
import { BusyService } from '../_services/busy.service';
import { environment } from 'src/environments/environment';

@Injectable()
export class LoadingInterceptor implements HttpInterceptor {
  constructor(private busyService: BusyService) {}

  intercept(
    request: HttpRequest<unknown>,
    next: HttpHandler
  ): Observable<HttpEvent<unknown>> {
    this.busyService.busy();

    return next.handle(request).pipe(
      environment.production ? identity : delay(1000),
      finalize(() => {
        this.busyService.idle();
      })
    );
  }
}
