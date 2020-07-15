const backendDomain = ''; // TODO - Add backend address

export const environment = {
  production: true,
  backendDomain,
  backendBaseUrl: `http://${backendDomain}`,
  backendBaseAPIUrl: `http://${backendDomain}/api`,
  clientId: '705654214740-hn4155dbddmj2puk52i2s7cjscvcs9db.apps.googleusercontent.com'
};