# create new project
npx create-nx-workspace

# create new application
npx nx g @nx/angular:app user --directory=apps/user

# create local libraries
npx nx g @nx/angular:library shared --directory=libs/shared --standalone

# generate service libs
npx nx generate @nrwl/angular:service services/auth/auth --project=shared

# run application
npx nx serve user

# install identityserver4
npm install oidc-client --save