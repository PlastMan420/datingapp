import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse, HTTP_INTERCEPTORS } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(req).pipe( // rxjs
            catchError(error => {
                if (error instanceof HttpErrorResponse) { // Application HTTP Error
                    if (error.status === 401) {
                        return throwError(error.statusText);
                    }
                    const applicationError = error.headers.get('Application-Error');
                    // fetch 'Application-Error' message in asp/helpers.Extensions class
                    if (applicationError) { // check if it's not empty
                    console.error(applicationError);
                    return throwError(applicationError);
                    }
                    const serverError = error.error.errors;
                    let modalStateErrors = ' ';
                    if (serverError && typeof serverError === 'object') { // check server error and verify type
                        // if it is then it's a modal state error
                        for (const key in serverError) { // loop inside object key
                            if (serverError[key]) {
                                modalStateErrors += serverError[key] + '\n';
                            }
                        }
                    }
                    return throwError(modalStateErrors || serverError || 'Server Error');
                    // API can throw modalstate error or server error
                }
            })
        );
    }
}

// Error interceptor provider to add to add into appmodule since we can't add the code above atm
export const ErrorInterceptorProvider = {
    provide: HTTP_INTERCEPTORS, // custom additional HTTP error interceptor
    useClass: ErrorInterceptor,
    multi: true // enabling support for array of interceptors. setting thisi to false overwrites other interceptors
};
