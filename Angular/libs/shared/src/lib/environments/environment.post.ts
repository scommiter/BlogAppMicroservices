export const environment = {
    production: false,
  
    appName: 'Angular Post Client',
  
    userUrl: 'https://localhost:7001',
    postUrl: 'https://localhost:7002',
    notificationUrl: 'https://localhost:7004',
  
    auth: {
      issuer: 'https://localhost:7000',
      selfUrl: 'http://localhost:4100',
      clientId: 'angular_post_client',
      dummyClientSecret: 'angularPostClient',
      scope: 'openid profile email address roles postAPI'
    }
};