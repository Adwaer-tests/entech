import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {API_BASE_URL, ApiClient} from "./api.client";
import {HttpClientModule} from "@angular/common/http";
import {environment} from "../../environments/environment";


@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    HttpClientModule
  ],
  providers: [
    ApiClient,
    {provide: API_BASE_URL, useValue: environment.ApiUrl},
  ]
})
export class CoreModule {
}
