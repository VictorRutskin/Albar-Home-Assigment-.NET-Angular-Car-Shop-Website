import { Car } from './../../Models/Car.model';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/app/Environments/myEnvironment';

@Injectable({
  providedIn: 'root',
})
export class CarsService {
  constructor(private http: HttpClient) {}

  GetAllCars(): Observable<Car[]> {
    return this.http.get<Car[]>(environment.ServerUrl + '/api/Car');
  }

  Get3CarExtras(): Observable<Car[]> {
    return this.http.get<Car[]>(environment.ServerUrl + '/api/Car/Top3');
  }

  GetImage(id : number) {
    return this.http.get(environment.ServerUrl + '/api/Car/image/'+id, { responseType: 'blob' });
  }
  
}
