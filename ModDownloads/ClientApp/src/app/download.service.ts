import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable()
export class DownloadService {

  private headers: HttpHeaders;
  private accessPointUrlDownloads: string = window.location.origin+ "/api/downloads";
  private accessPointUrlMods: string = window.location.origin+ "/api/mods";

  constructor(private http: HttpClient) {
    this.headers = new HttpHeaders({ 'Content-Type': 'application/json; charset=utf-8' });
  }

  public get() {
    // Get all jogging data
    return this.http.get(this.accessPointUrlDownloads, { headers: this.headers });
  }
  public getById(payload) {
    // Get all download data for a specifc mod
    return this.http.get(this.accessPointUrlMods + '/' + payload.id + "/downloads", { headers: this.headers });
  }
  public add(payload) {
    return this.http.post(this.accessPointUrlDownloads, payload, { headers: this.headers });
  }

  public remove(payload) {
    return this.http.delete(this.accessPointUrlDownloads + '/' + payload.id, { headers: this.headers });
  }

  public update(payload) {
    return this.http.put(this.accessPointUrlDownloads + '/' + payload.id, payload, { headers: this.headers });
  }
}
