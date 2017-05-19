import { ModuleWithProviders } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { PreparationListComponent } from './preparation-list/preparation-list.component';
import { PreparationComponent } from './preparation/preparation.component';

const appRoutes: Routes = [
  { path: '', component: PreparationListComponent, pathMatch: 'full'}, // redirect to home page on load
  { path: 'preparations', component: PreparationListComponent, pathMatch: 'full'},
  { path: '**', component: PageNotFoundComponent }
];

export const routing: ModuleWithProviders = RouterModule.forRoot(appRoutes);
