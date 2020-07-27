// TODO - Add production addresses
const backendDomain = 'localhost:6879'; 

export const environment = {
  production: true,
  backendDomain,
  backendBaseUrl: `http://${backendDomain}`,
  backendBaseAPIUrl: `http://${backendDomain}/api`,
  portalBaseUrl: 'http://localhost:4200',
  identityServerBaseUrl: 'https://localhost:5001'
};
