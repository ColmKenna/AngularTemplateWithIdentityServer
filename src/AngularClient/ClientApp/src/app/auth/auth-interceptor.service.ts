import { HttpEvent, HttpHandler, HttpHeaders, HttpInterceptor, HttpRequest, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ActivatedRoute,Router } from '@angular/router';
import { from, Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { Constants } from '../Constants';
import { AuthService } from './auth-service.component';

@Injectable()
export class AuthInterceptorService implements HttpInterceptor {
  private url: string;
  //private u
  constructor(private _authService: AuthService,
    private _router: Router,
    private route: ActivatedRoute,) {
    this.route.url.subscribe(activeUrl => {
      this.url = window.location.pathname;
    });
  }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    //return next.handle(req);

    
    if (req.url.startsWith(Constants.apiRoot)) {
      return from(this._authService.getAccessToken().then(token => {
        const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
        const authReq = req.clone({ headers });
        return next.handle(authReq).pipe(tap(_ => { }, error => {
          var respError = error as HttpErrorResponse;
          if (respError && (respError.status === 401 || respError.status === 403)) {
            let address = this.url;
            this._authService.login(address);
          }
        })).toPromise();
      }));
    }
    else {
      return next.handle(req);
    }
  }
}
