import { Car } from './../../Models/Car.model';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/app/Environments/myEnvironment';

@Injectable({
  providedIn: 'root',
})
export class CarsService {
  constructor(private http: HttpClient) {}

  // Gets header and adds the token
  private getHeaders(): HttpHeaders {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: 'Bearer ' + localStorage.getItem('jwt'),
    });
    return headers;
  }

  // Gets header and adds the token
  private getHeadersNoType(): HttpHeaders {
    const headers = new HttpHeaders({
      Authorization: 'Bearer ' + localStorage.getItem('jwt'),
    });
    return headers;
  }

  // Gets all cars
  public GetAllCars(): Observable<Car[]> {
    return this.http.get<Car[]>(environment.ServerUrl + '/api/Car');
  }

  // Gets 3 cars with highest units in stock value
  public Get3CarExtras(): Observable<Car[]> {
    return this.http.get<Car[]>(environment.ServerUrl + '/api/Car/Top3');
  }

  // Returns a single car using id
  public GetSingleCar(id: number): Observable<Car> {
    return this.http.get<Car>(environment.ServerUrl + '/api/Car/' + id);
  }

  // Returns a single car using parameters
  public GetSingleCar2(Car: Car): Observable<Car> {
    let params = new HttpParams()
      .set('Name', Car.name)
      .set('Category', Car.category)
      .set('Price', Car.price.toString());
    return this.http.get<Car>(environment.ServerUrl + '/api/Car/GetCar2', {
      params: params,
    });
  }

  // Returns an image using id
  public GetImage(id: number) {
    return this.http.get(environment.ServerUrl + '/api/Car/image/' + id, {
      responseType: 'blob',
    });
  }

  // Returns a car id using name
  public GetCarIdUsingName(searchString: string) {
    return this.http.get<number>(
      environment.ServerUrl + '/api/Car/GetCarWithName?name=' + searchString
    );
  }

  // Add new car (admin)
  public PostNewCar(NewCar: Car): Observable<Car> {
    const headers = this.getHeaders();
    return this.http.post<Car>(environment.ServerUrl + '/api/Car/', NewCar, {
      headers,
    });
  }

  // Add car image (admin)
  public PostImage(formData: FormData): Observable<any> {
    const headers = this.getHeadersNoType();
    return this.http.post(environment.ServerUrl + '/api/Car/Image', formData, {
      headers: headers,
      reportProgress: true,
      observe: 'events',
    });
  }

  // Buys one car (-1 units in stock value)
  public PutBuyOne(boughtCar: Car): Observable<Car> {
    return this.http.put<Car>(
      environment.ServerUrl + '/api/Car/Buy/' + boughtCar.id,
      boughtCar
    );
  }

  // Updates car values (admin)
  public PutUpdateCar(UpdatedCar: Car): Observable<Car> {
    const headers = this.getHeaders();
    return this.http.put<Car>(environment.ServerUrl + '/api/Car/' + UpdatedCar.id, UpdatedCar, {
      headers,
    });
  }

  // Deletes car (admin)
  public DeleteCar(id: number): Observable<Car> {
    const headers = this.getHeaders();
    return this.http.delete<Car>(environment.ServerUrl + '/api/Car/' + id, {
      headers,
    });
  }
}
