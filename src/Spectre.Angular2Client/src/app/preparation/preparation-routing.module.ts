import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { PreparationComponent } from './preparation.component';

const preparationRoutes = [
    { path: 'preparation/:id', component: PreparationComponent }
];

@NgModule({
    imports: [
        RouterModule.forChild(preparationRoutes)
    ],
    exports: [
        RouterModule
    ]
})

export class PreparationRoutingModule { }
