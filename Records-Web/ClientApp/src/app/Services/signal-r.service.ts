import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder, LogLevel } from '@aspnet/signalr';
import { HttpClient } from '@angular/common/http';
import { appConfig } from '../app.config';
import { map } from 'rxjs/operators';
import { Record } from '../Models/Record';
import { Subject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  constructor(private http: HttpClient) { }

  private hubConnection: HubConnection;
  private records = new Subject<Record[]>();

  public startConnection = () => {

    this.hubConnection = new HubConnectionBuilder()
      .configureLogging(LogLevel.Debug)
      .withUrl(appConfig.API.hub)
      .build();

    this.hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch(err => console.log('Error while starting connection: ' + err))
  }

  public addTransferChartDataListener = () => {
    this.hubConnection.on('transferdata', (records) => {
      this.updateRecordEnd(records);
    });
  }

  public startHttpRequest = () => {
    return this.http.get(appConfig.API.record.get, {}).pipe();
  }

  updateRecordEnd(records: Record[]) {
    this.records.next(records);
  }

  updateRecordEnded(): Observable<Record[]> {
    return this.records.asObservable();
  }
}
