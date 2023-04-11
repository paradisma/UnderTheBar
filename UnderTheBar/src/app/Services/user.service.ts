import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from '../Models/Core/user';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

  public getAllUsers() {
    return this.http.get<User[]>("https://localhost:7218/api/User/GetUsers");
  }
}
