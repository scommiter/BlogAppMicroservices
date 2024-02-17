# create new empty work space
ng new Angular --create-application=false

# create project
ng g application chat-app --routing --style=scss --standalone false
ng g application post-app --routing --style=scss --standalone false
ng g application user-app --routing --style=scss --standalone false
ng g application host-app --routing --style=scss --standalone false

# create library 
ng generate library my-lib

# run project
ng s host-app -o

# setup
npm i webpack webpack-cli --save-dev

ng add @angular-architects/module-federation --project host-app --port 4000
ng add @angular-architects/module-federation --project user-app --port 4100
ng add @angular-architects/module-federation --project post-app --port 4200
ng add @angular-architects/module-federation --project chat-app --port 4300

# generate component in specific project
ng g c dashboard --project=user-app --standalone false

# install IDP library
npm install oidc-client-ts --save

# install bootstrap
npm install bootstrap@5.3.2