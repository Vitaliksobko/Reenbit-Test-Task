import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { HttpClient } from "@angular/common/http";
import { LoginModel } from "../models/login.model";
import { RegistrationModel } from "../models/registration.model";
import { IdRoleModel } from "../models/idRole.model";
import { environment } from "../environments/environment";

@Injectable({
  providedIn: "root"
})
export class AuthorizationService {

  private apiUrl = `${environment.apiUrl}/api/auth`;

  constructor(private http: HttpClient) {}

  login(loginModel: LoginModel): Observable<IdRoleModel> {
    return this.http.post<IdRoleModel>(`${this.apiUrl}/login`, loginModel);
  }

  registration(registrationModel: RegistrationModel): Observable<string> {
    return this.http.post<string>(`${this.apiUrl}/registration`, registrationModel);
  }

  logout(): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/logout`, {});
  }
}

