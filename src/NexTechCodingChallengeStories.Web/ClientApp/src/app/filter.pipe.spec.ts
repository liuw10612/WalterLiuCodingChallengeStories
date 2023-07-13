import { FilterPipe } from './filter.pipe';

describe('FilterPipe', () => {
  it('create an instance', () => {
    const pipe = new FilterPipe();
    expect(pipe).toBeTruthy();
  });

  it('return 1 row USA', () => {
    // arrange
    let searchText='USA';
    let players = [
      { id: 11, name: 'Leela', country: 'India' },
      { id: 12, name: 'Narco', country: 'USA' },
      { id: 13, name: 'Bombasto', country: 'UK' },
      { id: 14, name: 'Celeritas', country: 'Canada' },
      { id: 15, name: 'Magneta', country: 'Russia' },
      { id: 16, name: 'Zhao Zhiqiang', country: 'China' },
      { id: 17, name: 'Dynama', country: 'Germany' },
      { id: 18, name: 'Dr IQ', country: 'Hong Kong' },
      { id: 19, name: 'Magma', country: 'South Africa' },
      { id: 20, name: 'Tornado', country: 'Sri Lanka' }
    ];
    let props = ['name', 'country'];

    const pipe = new FilterPipe();

    // action
    var result = pipe.transform(players, searchText, props);

    // assert
    expect(result.length).toEqual(1);
  });
  it('return 0 row : Walter', () => {
    // arrange
    let searchText = 'Walter';
    let players = [
      { id: 11, name: 'Leela', country: 'India' },
      { id: 12, name: 'Narco', country: 'USA' },
      { id: 13, name: 'Bombasto', country: 'UK' },
      { id: 14, name: 'Celeritas', country: 'Canada' },
      { id: 15, name: 'Magneta', country: 'Russia' },
      { id: 16, name: 'Zhao Zhiqiang', country: 'China' },
      { id: 17, name: 'Dynama', country: 'Germany' },
      { id: 18, name: 'Dr IQ', country: 'Hong Kong' },
      { id: 19, name: 'Magma', country: 'South Africa' },
      { id: 20, name: 'Tornado', country: 'Sri Lanka' }
    ];
    let props = ['name', 'country'];

    const pipe = new FilterPipe();

    // action
    var result = pipe.transform(players, searchText, props);

    // assert
    expect(result.length).toEqual(0);
  });
});
