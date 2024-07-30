import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { LocalService } from '../services/local.service';

export const authGuard: CanActivateFn = (route, state) => {
  const localService = inject(LocalService);
  const router = inject(Router);

  if (localService.isAuthenticated(LocalService.AuthId)) {
    return true;
  } else {
    router.navigate(['/login']); 
    return false;
  }
};