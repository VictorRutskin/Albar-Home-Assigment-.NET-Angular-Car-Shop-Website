import { Car } from './../../Models/Car.model';
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
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

  GetSingleCar(id: number): Observable<Car> {
    return this.http.get<Car>(environment.ServerUrl + '/api/Car/' + id);
  }
  GetSingleCar2(Car:Car): Observable<Car> {
    let params = new HttpParams()
      .set('Name', Car.name)
      .set('Category', Car.category)
      .set('Price', Car.price.toString());
    return this.http.get<Car>(environment.ServerUrl + '/api/Car/GetCar2', {params: params});
  }

  GetImage(id: number) {
    return this.http.get(environment.ServerUrl + '/api/Car/image/' + id, {
      responseType: 'blob',
    });
  }

  GetCarIdUsingName(searchString:string) {
    return this.http.get<number>(environment.ServerUrl +'/api/Car/GetCarWithName?name='+searchString)
  }

  PostNewCar(NewCar:Car) : Observable<Car>{
    return this.http.post<Car>(environment.ServerUrl + '/api/Car/',NewCar);
  }

  PutBuyOne(boughtCar:Car) : Observable<Car>{
    return this.http.put<Car>(environment.ServerUrl + '/api/Car/' + boughtCar.id,boughtCar);
  }

  PutUpdateCar(UpdatedCar:Car) : Observable<Car>{
    return this.http.put<Car>(environment.ServerUrl + '/api/Car/' + UpdatedCar.id,UpdatedCar);
  }

  DeleteCar(id:number) : Observable<Car>{
    return this.http.delete<Car>(environment.ServerUrl + '/api/Car/' + id);
  }
}
