import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { PreparationComponent } from './preparation.component';

import { PreparationRoutingModule } from './preparation-routing.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    PreparationRoutingModule
  ],
  declarations: [
    PreparationComponent
  ],
  providers: [  ]
})
export class PreparationsModule {}
