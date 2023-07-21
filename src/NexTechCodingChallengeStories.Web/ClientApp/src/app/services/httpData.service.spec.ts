import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';

interface Data {
  name: string;
}

const testUrl = 'https://github.com/HackerNews/API';

describe('HttpClient testing', () => {
  let httpClient: HttpClient;
  let httpTestingController: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule]
    });

    // Inject the http service and test controller for each test
    httpClient = TestBed.inject(HttpClient);
    httpTestingController = TestBed.inject(HttpTestingController);
  });
  afterEach(() => {
    // After every test, assert that there are no more pending requests.
    httpTestingController.verify();
  });

  it('should call the testurl with get Request', () => {
    const testData: Data = { name: 'Walter Liu Test Data' };

    // Make an HTTP GET request
    httpClient.get<Data>(testUrl)
      .subscribe(data =>
        // When observable resolves, result should match test data
        expect(data).toEqual(testData)
      );

    const req = httpTestingController.expectOne(
      { method: 'GET', url: 'https://github.com/HackerNews/API' });

    // Assert that the request is a GET.
    expect(req.request.method).toEqual('GET');

    // Respond with mock data, causing Observable to resolve.
    // Subscribe callback asserts that correct data was returned.
    req.flush(testData);

    // Finally, assert that there are no outstanding requests.
    httpTestingController.verify();
  });
    
  it('should test multiple requests', () => {
    const testData: Data[] = [
      { name: 'Walter' }, { name: 'Lily' }
    ];

    // Make three requests in a row
    httpClient.get<Data[]>(testUrl)
      .subscribe(d => expect(d.length).withContext('should have no data').toEqual(0));

    httpClient.get<Data[]>(testUrl)
      .subscribe(d => expect(d).withContext('should be one element array').toEqual([testData[0]]));

    httpClient.get<Data[]>(testUrl)
      .subscribe(d => expect(d).withContext('should be expected data').toEqual(testData));

    // #docregion multi-request
    // get all pending requests that match the given URL
    const requests = httpTestingController.match(testUrl);
    expect(requests.length).toEqual(3);

    requests[0].flush([]);
    requests[1].flush([testData[0]]);
    requests[2].flush(testData);

  });

    it('should test for 404 error', () => {
    const emsg = 'deliberate 404 error';

    httpClient.get<Data[]>(testUrl).subscribe({
      next: () => fail('should have failed with the 404 error'),
      error: (error: HttpErrorResponse) => {
        expect(error.status).withContext('status').toEqual(404);
        expect(error.error).withContext('message').toEqual(emsg);
      },
    });

    const req = httpTestingController.expectOne(testUrl);

    // Respond with mock error
    req.flush(emsg, { status: 404, statusText: 'Not Found' });
  });
  
  // test network-error
  it('can test for network error', done => {
    // Create mock ProgressEvent with type `error`, raised when something goes wrong
    // at network level. e.g. Connection timeout, DNS error, offline, etc.
    const mockError = new ProgressEvent('error');

    httpClient.get<Data[]>(testUrl).subscribe({
      next: () => fail('should have failed with the network error'),
      error: (error: HttpErrorResponse) => {
        expect(error.error).toBe(mockError);
        done();
      },
    });

    const req = httpTestingController.expectOne(testUrl);

    // Respond with mock error
    req.error(mockError);
  });
  // #enddocregion network-error

  it('httpTestingController.verify should fail if HTTP response not simulated', () => {
    // Sends request
    httpClient.get('some/api').subscribe();

    // verify() should fail because haven't handled the pending request.
    expect(() => httpTestingController.verify()).toThrow();

    // Now get and flush the request so that afterEach() doesn't fail
    const req = httpTestingController.expectOne('some/api');
    req.flush(null);
  });
});

