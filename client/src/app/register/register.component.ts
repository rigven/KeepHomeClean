import { NgIf } from '@angular/common';
import { Component, inject, OnInit, signal } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, FormControl, FormsModule, ReactiveFormsModule, ValidatorFn, Validators } from '@angular/forms';
import { AccountService } from '../_services/account.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule, NgIf],
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent implements OnInit{
  private accountService = inject(AccountService);
  private fb = inject(FormBuilder);
  private router = inject(Router);
  registerForm: FormGroup = new FormGroup({});
  validationErrors: string[] | undefined;

  ngOnInit(): void {
      this.initializeForm();
  }

  initializeForm() {
    this.registerForm = this.fb.group({
      userName: ['', Validators.required],
      knownAs: ['', Validators.required],
      password: ['', [Validators.required, Validators.minLength(8), this.matchPasswordRegex()]],
      confirmPassword: ['', [Validators.required, this.matchValues('password')]]
    });
    this.registerForm.controls['password'].valueChanges.subscribe({
      next: () => this.registerForm.controls['confirmPassword'].updateValueAndValidity()
    })
  }

  matchValues(matchTo: string): ValidatorFn {
    //Returning the function
    return (control: AbstractControl) => {
      // Returning 'true' means that this control do not match
      return control.value === control?.parent?.get(matchTo)?.value ? null : {isMatching: true}
    }
  }

  matchPasswordRegex(): ValidatorFn {
    //Returning the function
    return (control: AbstractControl) => {
      // Returning 'true' means that this control do not match
      let pattern = /^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).*$/;
      return pattern.test(control.value) ? null : {passwordRegex: true}
    }
  }

  register() {
    this.accountService.register(this.registerForm.value).subscribe({
      next: _ => { this.router.navigateByUrl('/') },
      error: error => {
        this.validationErrors = error;
      }
    })
  }
}
