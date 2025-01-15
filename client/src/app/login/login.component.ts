import { NgIf } from '@angular/common';
import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, NgIf],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  private accountService = inject(AccountService);
  private router = inject(Router);
  userName: string = "";
  password: string = "";
  errors: string[] | undefined;

  login() {
    this.accountService.login({username: this.userName, password: this.password}).subscribe({
      next: _ => { this.router.navigateByUrl('/') },
      error: error => {
        this.errors = error;
      }
    })
  }
}
