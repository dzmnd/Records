let API: any = {
  baseUrl: 'http://localhost:62040'
};

API.record = {
  base: `api/Record`
};

API.record.get = `${API.baseUrl}/${API.record.base}`;
API.record.getRecords = `${API.baseUrl}/${API.record.base}/GetRecords`;
API.record.updateRecords = `${API.baseUrl}/${API.record.base}/UpdateRecords`;

API.hub = `${API.baseUrl}/ModifyRecord`;

export const appConfig: any = {
  API: API
};
