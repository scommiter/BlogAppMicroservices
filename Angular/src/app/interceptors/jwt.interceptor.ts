import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, take } from "rxjs";
import { User } from "oidc-client-ts";
import { AuthService } from "../service/auth.service";

@Injectable()
export class JwtInterceptor implements HttpInterceptor {

  constructor(private authService: AuthService) {}

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
