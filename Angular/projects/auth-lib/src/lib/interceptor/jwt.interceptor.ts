import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { AuthLibService } from "../auth-lib.service";
import { Observable, take } from "rxjs";
import { User } from "oidc-client-ts";

@Injectable()
export class JwtInterceptor implements HttpInterceptor {

  constructor(private authService: AuthLibService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    let currentUser!: User
    this.authService.currentUser$.pipe(take(1)).subscribe(user => currentUser = user);
    
    if(currentUser){
      request = request.clone({
        setHeaders:{
          Authorization: `Bearer ${currentUser.access_token}`
        }
      });
    }
    return next.handle(request);
  }
}
