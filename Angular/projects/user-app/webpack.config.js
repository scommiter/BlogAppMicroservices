const ModuleFederationPlugin = require("webpack/lib/container/ModuleFederationPlugin");
const mf = require("@angular-architects/module-federation/webpack");
const path = require("path");
const share = mf.share;

const sharedMappings = new mf.SharedMappings();
sharedMappings.register(
  path.join(__dirname, '../../tsconfig.json'),
  [/* mapped paths to share */]);

module.exports = {
  output: {
    uniqueName: "userApp",
    publicPath: "auto",
    scriptType: "text/javascript"
  },
  optimization: {
    runtimeChunk: false
  },   
  resolve: {
    alias: {
      ...sharedMappings.getAliases(),
    }
  },
  experiments: {
    outputModule: true
  },
  plugins: [
    new ModuleFederationPlugin({
        // library: { type: "module" },

        // For remotes (please adjust)
        name: "userApp",
        filename: "remoteEntry.js",
        exposes: {
          './DashboardModule': './projects/user-app/src/app/dashboard/dashboard.module.ts',
        },        
        
        // For hosts (please adjust)
        // remotes: {
        //     "hostApp": "http://localhost:4000/remoteEntry.js",
        //     "chatApp": "http://localhost:4300/remoteEntry.js",
        //     "postApp": "http://localhost:4200/remoteEntry.js",

        // },

        shared: share({
          "@angular/core": { singleton: true, strictVersion: true, requiredVersion: 'auto' }, 
          "@angular/common": { singleton: true, strictVersion: true, requiredVersion: 'auto' }, 
          "@angular/common/http": { singleton: true, strictVersion: true, requiredVersion: 'auto' }, 
          "@angular/router": { singleton: true, strictVersion: true, requiredVersion: 'auto' },
          // ...shareAll({ singleton: true, strictVersion: true, requiredVersion: 'auto' }),

          ...sharedMappings.getDescriptors()
        })
        
    }),
    sharedMappings.getPlugin()
  ],
};
