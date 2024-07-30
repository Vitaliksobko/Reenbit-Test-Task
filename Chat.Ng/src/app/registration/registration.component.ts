import { Component } from '@angular/core';
import { LocalService } from '../../services/local.service';
import { AuthorizationService } from '../../services/authorization.service';
import { RegistrationModel } from '../../models/registration.model';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrl: './registration.component.scss'
})
export class RegistrationComponent {

  constructor(
    private AuthorizationService: AuthorizationService,
    private localService: LocalService
   ){}

  errorMessage: string = '';
  registrationModel = new RegistrationModel();

  onRegistration(){
    this.AuthorizationService.registration(this.registrationModel).subscribe(data =>{
      this.localService.put(LocalService.AuthId ,data);
      

     
      window.location.href = '/chat'
    },
  errorResponse => {
    this.errorMessage = errorResponse.error;
  })
  }
}

