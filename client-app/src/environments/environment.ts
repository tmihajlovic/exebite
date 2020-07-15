// This file can be replaced during build by using the `fileReplacements` array.
// `ng build --prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

const backendDomain = 'localhost:6879';

export const environment = {
  production: false,
  backendDomain: backendDomain,
  backendBaseUrl: `http://${backendDomain}`,
  backendBaseAPIUrl: `http://${backendDomain}/api`,
  clientId: '705654214740-hn4155dbddmj2puk52i2s7cjscvcs9db.apps.googleusercontent.com'
};

/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
// import 'zone.js/dist/zone-error';  // Included with Angular CLI.
