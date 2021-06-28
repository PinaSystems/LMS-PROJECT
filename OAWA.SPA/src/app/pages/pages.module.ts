import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { routing } from './pages.routing';

import { LayoutModule } from '../shared/layout.module';
import { SharedModule } from '../shared/shared.module';

/* components */
import { PagesComponent } from './pages.component';
import { LoginComponent } from './login/login.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';
import { ResetPasswordComponent } from './reset-password/reset-password.component';
import { FormModule } from './form/form.module';



@NgModule({
    imports: [
        CommonModule,
        LayoutModule,
        SharedModule,
        routing,
        FormsModule,
        ReactiveFormsModule,
        FormModule
    ],
    declarations: [
        PagesComponent,
        LoginComponent,
        ForgotPasswordComponent,
        ResetPasswordComponent
    ]
})
export class PagesModule { }
