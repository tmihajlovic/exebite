import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { AuthService } from '../services/auth.service';
import { Observable, from } from 'rxjs';
import { switchMap } from 'rxjs/operators';

@Injectable()
export class AccessTokenInterceptor implements HttpInterceptor {

  constructor(public auth: AuthService) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return from(this.auth.getAuthorizationHeaderValue()).pipe(
      switchMap(
        authHeader => {
          req = req.clone({
            setHeaders: {
              Authorization: authHeader
            }
          });
          return next.handle(req);
        }
      )
    )
  }
}