import {environment as baseEnv} from "./environment.user";

export const environment = {
  ...baseEnv,

  production: true,
  userUrl: '<USER_URL>',
  postUrl: '<POST_URL>',
  notificationUrl: '<NOTIFICATION_URL>',

  auth: {
    ...baseEnv.auth,
    issuer: '<ISSUER_URL>',
    selfUrl: '<SELF_URL>'
  }
};
