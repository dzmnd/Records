import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';
import { appConfig } from '../app.config';
import { Record } from '../Models/Record';

@Injectable()
export class RecordService {
  constructor(private http: HttpClient) { }

  getRecords() {
    return this.http.get(appConfig.API.record.getRecords, {})
      .pipe(map((res: Response) => {
        return res;
      }));
  }

  updateRecords(records: Record[]) {
    return this.http.post(appConfig.API.record.updateRecords, records)
      .pipe(map((res: Response) => {
        return res;
      }));
  }
}
