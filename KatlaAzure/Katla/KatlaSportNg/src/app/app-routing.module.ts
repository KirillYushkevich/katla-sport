import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MainPageComponent } from 'app/main-page/main-page.component';
import { HiveFormComponent } from './hive-management/forms/hive-form.component';
import { HiveSectionFormComponent } from './hive-management/forms/hive-section-form.component';
import { HiveListComponent } from './hive-management/lists/hive-list.component';
import { HiveSectionListComponent } from './hive-management/lists/hive-section-list.component';
import { ProductCategoryFormComponent } from './product-management/forms/product-category-form.component';
import { ProductFormComponent } from './product-management/forms/product-form.component';
import { ProductCategoryListComponent } from './product-management/lists/product-category-list.component';
import { ProductCategoryProductListComponent } from './product-management/lists/product-category-product-list.component';
import { ProductListComponent } from './product-management/lists/product-list.component';
import { DoctorListComponent } from './doctor-managment/lists/doctor-list.component';
import { DoctorFormComponent } from './doctor-managment/form/doctor-form.component';
import { DepartmentListComponent } from './department-managment/lists/department-list.component';
import { DepartmentFormComponent } from './department-managment/form/department-form.component';
import { PatientListComponent } from './patient-managment/lists/patient-list.component';
import { PatientFormComponent } from './patient-managment/form/patient-form.component';

const routes: Routes = [
  { path: '', redirectTo: '/main', pathMatch: 'full' },
  { path: 'main', component: MainPageComponent },
  { path: 'categories', component: ProductCategoryListComponent },
  { path: 'category', component: ProductCategoryFormComponent },
  { path: 'category/:id', component: ProductCategoryFormComponent },
  { path: 'category/:id/products', component: ProductCategoryProductListComponent },
  { path: 'products', component: ProductListComponent },
  { path: 'product/:id', component: ProductFormComponent },
  { path: 'category/:categoryId/product/:id', component: ProductFormComponent },
  { path: 'hives', component: HiveListComponent },
  { path: 'hive', component: HiveFormComponent },
  { path: 'hive/:id', component: HiveFormComponent },
  { path: 'hive/:id/sections', component: HiveSectionListComponent },
  { path: 'hive/:store-hive-id/section', component: HiveSectionFormComponent },
  { path: 'hive/:store-hive-id/section/:id', component: HiveSectionFormComponent },
  { path: 'doctors', component: DoctorListComponent },
  { path: 'doctor/:id', component: DoctorFormComponent },
  { path: 'doctor', component: DoctorFormComponent },
  { path: 'departments', component: DepartmentListComponent },
  { path: 'department/:id', component: DepartmentFormComponent },
  { path: 'department', component: DepartmentFormComponent },
  { path: 'patients', component: PatientListComponent },
  { path: 'patient/:id', component: PatientFormComponent },
  { path: 'patient', component: PatientFormComponent },
];

@NgModule({
  imports: [ RouterModule.forRoot(routes) ],
  exports: [ RouterModule ]
})
export class AppRoutingModule { }