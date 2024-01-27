import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';
import { appRoutes } from './app.routes';
import { AuthConfig, OAuthStorage } from 'angular-oauth2-oidc';
import { APP_NAME, AuthLocalStorageService, authCodeFlowConfig, environment } from '@angular/shared';

export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(appRoutes),
    {
      provide: AuthConfig,
      useValue: authCodeFlowConfig
    },
    {
      provide: OAuthStorage,
      useClass: AuthLocalStorageService,
    },
    {
      provide: APP_NAME,
      useValue: environment.appName,
    }
  ],
};
