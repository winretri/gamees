type ParamValue = string | number;

interface Params {
  [key: string]: ParamValue;
}

export class Url {
  private readonly _path: Array<ParamValue>;
  private readonly _params: Params;

  constructor(path: Array<ParamValue>, params: Params) {
    this._path = path;
    this._params = params;
  }

  public static init() {
    return new Url([], {});
  }

  public addPaths(...paths: ParamValue[]): Url {

    return new Url([...this._path, ...paths], this._params);
  }

  public setParam(key: string, val: ParamValue): Url {
    const copy = {...this._params};
    copy[key] = val;
    return new Url(this._path, copy);
  }

  public serialize(): string {
    const param: string[] = Object.keys(this._params).map((key: string) => {
      return encodeURIComponent(key) + '=' + encodeURIComponent(this._params[key] + '');
    });
    return this._path.filter(e => e).join('/') + ((param.length) ? '?' + (param).join('&') : '');
  }
}
