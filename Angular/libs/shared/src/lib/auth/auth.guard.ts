import { inject } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivateChildFn, CanActivateFn, Router, RouterStateSnapshot } from "@angular/router";
import { OAuthService } from "angular-oauth2-oidc";
import { from, map } from "rxjs";

export const canActivate: CanActivateFn = (
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
    ) => {
    const oauthService = inject(OAuthService);
    const router = inject(Router);

    if(oauthService.hasValidAccessToken() && oauthService.hasValidIdToken()){
        return true;
    }

    return from(oauthService.loadDiscoveryDocumentAndTryLogin()).pipe(
        map(() => {
            if (oauthService.hasValidAccessToken() && oauthService.hasValidIdToken()) {
                return true;
            }
    
            return router.createUrlTree(['/login'], {queryParams: {returnUrl: state.url}});
        })
    );
};

export const canActivateChild: CanActivateChildFn = (route: ActivatedRouteSnapshot, state: RouterStateSnapshot) => canActivate(route, state);
