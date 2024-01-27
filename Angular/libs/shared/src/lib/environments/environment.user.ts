export const environment = {
    production: false,
  
    appName: 'Angular User Client',
  
    userUrl: 'https://localhost:7001',
    postUrl: 'https://localhost:7002',
    notificationUrl: 'https://localhost:7004',
  
    auth: {
      issuer: 'https://localhost:7000',
      selfUrl: 'http://localhost:4200',
      clientId: 'angular_user_client',
      dummyClientSecret: 'angularUserClient',
      scope: 'openid profile email address roles userAPI'
    }
};