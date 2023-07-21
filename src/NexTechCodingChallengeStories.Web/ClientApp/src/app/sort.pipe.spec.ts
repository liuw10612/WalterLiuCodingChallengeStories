import { SortPipe } from './sort.pipe';

describe('SortPipe', () => {
  it('create an instance', () => {
    const pipe = new SortPipe();
    expect(pipe).toBeTruthy();
  });

  it('ascending sort return 1st row to be Bombasto', () => {
    // arrange
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
    let props = ['name', true];

    const pipe = new SortPipe();

    // action
    var result = pipe.transform(players, props);

    // assert
    expect(result[0].name).toEqual('Bombasto');
  });
  it('descending sort return 1st row to be USA', () => {
    // arrange
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
    let props = ['country', false];

    const pipe =  new SortPipe();

    // action
    var result = pipe.transform(players,  props);

    // assert
    expect(result[0].country).toEqual('USA');
  });
});
