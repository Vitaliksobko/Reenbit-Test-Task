import { Component } from '@angular/core';
import { LocalService } from '../../services/local.service';
import { AuthorizationService } from '../../services/authorization.service';
import { LoginModel } from '../../models/login.model';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {

  constructor(
    private AuthorizationService: AuthorizationService,
    private localService: LocalService,
    ){}

  
  loginModel = new LoginModel();
  errorMessage: string = '';


  onLogin(){
    this.AuthorizationService.login(this.loginModel).subscribe(data => {
      this.localService.put(LocalService.AuthId,data.userId);
      

      window.location.href = '/chat'
      
    },
    errorResponse => {
      this.errorMessage = errorResponse.error;
    })
  }
}