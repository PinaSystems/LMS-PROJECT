import { Routes, RouterModule } from '@angular/router';
import { PagesComponent } from './pages.component';
import { LoginComponent } from './login/login.component';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';
import { ResetPasswordComponent } from './reset-password/reset-password.component';

export const childRoutes: Routes = [
    {
        path: 'login',
        component: LoginComponent,
    },
    {
        path: 'password/forgot',
        component: ForgotPasswordComponent,
    },
    {
        path: 'password/reset',
        component: ResetPasswordComponent,
    },
    {
        path: 'pages',
        component: PagesComponent,
        children: [
            { path: '', redirectTo: 'index', pathMatch: 'full' },
            { path: 'index', loadChildren: './index/index.module#IndexModule' },
            { path: 'editor', loadChildren: './editor/editor.module#EditorModule' },
            { path: 'icon', loadChildren: './icon/icon.module#IconModule' },
            { path: 'profile', loadChildren: './profile/profile.module#ProfileModule' },
            { path: 'form', loadChildren: './form/form.module#FormModule' },
            { path: 'charts', loadChildren: './charts/charts.module#ChartsModule' },
            { path: 'ui', loadChildren: './ui/ui.module#UIModule' },
            { path: 'table', loadChildren: './table/table.module#TableModule' },
            { path: 'menu-levels', loadChildren: './menu-levels/menu-levels.module#MenuLevelsModule' },
           
            { path: 'newsletter', loadChildren: './newsletter/newsletter.module#NewsletterModule' },
            { path: 'content', loadChildren: './content/content.module#ContentModule' },
            { path: 'chat', loadChildren: './chat/chat.module#ChatModule' },
            { path: 'contactus', loadChildren: './contactus/contactus.module#ContactusModule' },
            { path: 'assignment', loadChildren: './assessment/assessment.module#AssessmentModule' },
        ]
    }
];

export const routing = RouterModule.forChild(childRoutes);
