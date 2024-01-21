# create new empty work space
ng new Angular --create-application=false

# create project
ng g application chat-app --routing --style=scss
ng g application post-app --routing --style=scss
ng g application user-app --routing --style=scss

# run project
ng s user-app -o

# setup
npm i webpack webpack-cli --save-dev

ng add @angular-architects/module-federation --project user-app --port 4200
ng add @angular-architects/module-federation --project post-app --port 4300
ng add @angular-architects/module-federation --project chat-app --port 4400

# generate component in specific project
ng g c src/app/user-dashboard --project=user-app

# install IDP library
npm install oidc-client --save