import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { DownloadService } from './download.service';
import { ModService } from './mod.service';
import { NgxChartsModule } from '@swimlane/ngx-charts';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { DetailComponent } from './detail/detail.component';
import { ModComponent } from './mod/mod.component';
import { LoginFormComponent } from './login-form/login-form.component';

import { AuthGuard } from './auth.guard';
import { JwtHelperService, JwtModule } from '@auth0/angular-jwt';


import { ChartsModule } from 'ng2-charts-x';
@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    DetailComponent,
    ModComponent,
    LoginFormComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule, 
    FormsModule,
    RouterModule.forRoot([
      { path: 'detail/:name', component: DetailComponent, pathMatch: 'full' },
      { path: 'mods', component: ModComponent, pathMatch: 'full', canActivate: [AuthGuard]},
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'login', component: LoginFormComponent, pathMatch: 'full' },
    ]),
    JwtModule.forRoot({
      config: {
        tokenGetter: () => {
          return localStorage.getItem('auth_token');
        },
        whitelistedDomains: ['localhost:5001']
      }
    }),
    NgxChartsModule,
    BrowserAnimationsModule,
    ChartsModule
  ],
  providers: [
    DownloadService,
    ModService,
    AuthGuard,
    JwtHelperService

  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
