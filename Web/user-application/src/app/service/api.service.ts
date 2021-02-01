import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {User} from '../model/user.model';
import {Observable} from 'rxjs/index';
import { environment } from 'src/environments/environment';

@Injectable()
export class ApiService {

  constructor(private http: HttpClient) { }
  baseUrl = environment.baseUrl;

  getUsers(): Observable<User[]> {
    return this.http.get<User[]>(this.baseUrl);
  }

  getUserById(id: number): Observable<User> {
    return this.http.get<User>(this.baseUrl + id);
  }

  createUser(user: User): Observable<number> {
    return this.http.post<number>(this.baseUrl, user);
  }

  updateUser(user: User): Observable<number> {
    return this.http.put<number>(this.baseUrl, user);
  }

  deleteUser(id: number): Observable<number> {
    return this.http.delete<number>(this.baseUrl + `?userId=${id}`);
  }
}
