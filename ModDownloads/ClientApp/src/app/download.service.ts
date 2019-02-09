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
    return this.http.get(this.accessPointUrlDownloads, { headers: this.headers });
  }
  public getTotal() {
    return this.http.get(this.accessPointUrlDownloads+ '/Total', { headers: this.headers });
  }
  public getDaily() {
    return this.http.get(this.accessPointUrlDownloads + '/daily', { headers: this.headers });
  }
  public getMonthly() {
    return this.http.get(this.accessPointUrlDownloads + '/monthly', { headers: this.headers });
  }
  public getYearly() {
    return this.http.get(this.accessPointUrlDownloads + '/yearly', { headers: this.headers });
  }
  public getTotalById(payload) {
    return this.http.get(this.accessPointUrlMods + '/' + payload.id + "/downloads/Total", { headers: this.headers });
  }
  public getDailyById(payload) {
    return this.http.get(this.accessPointUrlMods + '/' + payload.id + "/downloads/daily", { headers: this.headers });
  }
  public getMonthlyById(payload) {
    return this.http.get(this.accessPointUrlMods + '/' + payload.id + "/downloads/monthly", { headers: this.headers });
  }
  public getYearlyById(payload) {
    return this.http.get(this.accessPointUrlMods + '/' + payload.id + "/downloads/yearly", { headers: this.headers });
  }
  public getById(payload) {
    return this.http.get(this.accessPointUrlMods + '/' + payload.id + "/downloads", { headers: this.headers });
  }
  public getIncreaseById(payload) {
    return this.http.get(this.accessPointUrlMods + '/' + payload.id + "/downloads/increase", { headers: this.headers });
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
