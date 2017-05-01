import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

import { AppComponent } from './app.component';
import { PreparationListComponent } from './preparation-list/preparation-list.component';
import { PreparationsService } from './preparation-list/preparation.service';

@NgModule({
  declarations: [
    AppComponent,
    PreparationListComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule
  ],
  providers: [PreparationsService],
  bootstrap: [AppComponent]
})
export class AppModule { }
