import { Injectable, Query } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Country } from '../models/Country';
import { Payload } from '../models/Payload';
import { Penalty } from '../models/Penalty';

@Injectable({
  providedIn: 'root'
})
export class ApiServiceService {

  constructor(private http: HttpClient) { }

  //URL to our API's
  countriesUrl = "https://localhost:44316/api/Penalty/GetAllCountries";
  penaltyUrl = "https://localhost:44316/api/Penalty/CalculateAmount"

  //Calls the API to get the list of all countries for display in dropdown menu
  getAllCountries():Observable<Country []>{
    return this.http.get<Country []>(this.countriesUrl); 
  }

  //Calls the API to calculate penalty amount
  calculatePenalty(query: Payload): Observable<Penalty>{
    return this.http.post<Penalty>(this.penaltyUrl, query );
  }



}