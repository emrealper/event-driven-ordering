# alpine/bombardier

Auto trigger with latest release and build a docker image with the [bombardier](https://github.com/codesenberg/bombardier) ready to run.

### Repo:

https://github.com/alpine-docker/bombardier

### Running

```
$ docker run --rm -it alpine/bombardier --help

$ docker run -ti --rm alpine/bombardier -c 200 -d 10s -l http://www.google.com
Bombarding http://www.google.com for 10s using 200 connections
[============================================================================================================] 10s
Done!
Statistics        Avg      Stdev        Max
  Reqs/sec       317.90     139.54     811.33
  Latency      614.37ms   147.09ms      1.81s
  Latency Distribution
     50%   613.75ms
     75%   639.65ms
     90%   707.76ms
     99%      1.20s
  HTTP codes:
    1xx - 0, 2xx - 0, 3xx - 3359, 4xx - 0, 5xx - 0
    others - 0
  Throughput:   256.01KB/s
```


