import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Password } from '../models/password.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PasswordService {
  private apiUrl = 'https://localhost:7105/api/PasswordManager';

  constructor(private http: HttpClient) {}

  getAll(): Observable<Password[]> {
    return this.http.get<Password[]>(this.apiUrl);
  }

  getById(id: number): Observable<Password> {
  return this.http.get<Password>(`${this.apiUrl}/${id}?decrypt=true`);
  }

  add(password: Password): Observable<any> {
    return this.http.post(this.apiUrl, password);
  }

  update(id: number, password: Password): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, password);
  }

  delete(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}
