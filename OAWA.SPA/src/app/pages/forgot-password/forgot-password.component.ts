import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.scss']
})
export class ForgotPasswordComponent implements OnInit {

  forgotForm: FormGroup;
  constructor(private fb: FormBuilder) { }

  ngOnInit() {
    this.forgotForm= this.fb.group(
     {email: new FormControl()} 
    )

    {{this.forgotForm}}
  }

}
