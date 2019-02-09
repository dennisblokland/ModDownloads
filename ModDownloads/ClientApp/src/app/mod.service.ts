import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ModService {

  private headers: HttpHeaders;
  private accessPointUrlMods: string = window.location.origin + "/api/mods";

  constructor(private http: HttpClient) {
    this.headers = new HttpHeaders({ 'Content-Type': 'application/json; charset=utf-8' });
  }

  public get() {
    return this.http.get(this.accessPointUrlMods, { headers: this.headers });
  }
  public getById(payload) {
    return this.http.get(this.accessPointUrlMods + '/' + payload.id , { headers: this.headers });
  }
  public getByName(name) {
    return this.http.get(this.accessPointUrlMods + '/byName/' + name, { headers: this.headers });
  }
  public add(payload) {
    return this.http.post(this.accessPointUrlMods, payload, { headers: this.headers });
  }

  public remove(payload) {
    return this.http.delete(this.accessPointUrlMods + '/' + payload.id, { headers: this.headers });
  }

  public update(payload) {
    return this.http.put(this.accessPointUrlMods + '/' + payload.id, payload, { headers: this.headers });
  }
}
