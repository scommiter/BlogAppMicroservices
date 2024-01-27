export const environment = {
    production: false,
  
    appName: 'Angular Notification Client',
  
    userUrl: 'https://localhost:7001',
    postUrl: 'https://localhost:7002',
    notificationUrl: 'https://localhost:7004',
  
    auth: {
      issuer: 'https://localhost:7000',
      selfUrl: 'http://localhost:4000',
      clientId: 'angular_notification_client',
      dummyClientSecret: 'angularNotificationClient',
      scope: 'openid profile email address roles notificationAPI'
    }
};