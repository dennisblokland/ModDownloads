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
import { ChartsModule } from 'ng2-charts-x';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    DetailComponent,
    ModComponent,

  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'detail/:name', component: DetailComponent, pathMatch: 'full' },
      { path: 'mods', component: ModComponent, pathMatch: 'full' },
    ]),
    NgxChartsModule,
    BrowserAnimationsModule,
    ChartsModule
  ],
  providers: [
    DownloadService,
    ModService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
