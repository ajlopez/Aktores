# Aktores

Akka-like actor model implementation in C#.

Work in Progress

## Samples

- [Pi](https://github.com/ajlopez/Aktores/tree/master/Samples/Pi) Pi calculation sample.

## References

### Akka

- [Akka](http://akka.io/)
- [Actors](http://doc.akka.io/docs/akka/snapshot/scala/actors.html)
- [Akka Scala - Getting Started Tutorial](http://doc.akka.io/docs/akka/2.0.2/intro/getting-started-first-scala.html)
- [Akka Java - Getting Started Tutorial](http://doc.akka.io/docs/akka/2.0.2/intro/getting-started-first-java.html)
- [Akka Documentation](http://akka.io/docs/)
- [Akka 2.2.3 Scala Documentation](http://doc.akka.io/docs/akka/2.2.3/scala.html)
- [Akka 2.0 ActorSystem](http://doc.akka.io/api/akka/2.0/akka/actor/ActorSystem.html)
- [Akka 2.0 ActorRef](http://doc.akka.io/api/akka/2.0/akka/actor/ActorRef.html)
- [Akka 2.0 ActorContext](http://doc.akka.io/api/akka/2.0/akka/actor/ActorContext.html)
- [Akka 2.0 ActorRefFactory](http://doc.akka.io/api/akka/2.0/akka/actor/ActorRefFactory.html)
- [Akka 2.0 Actor](http://doc.akka.io/api/akka/2.0/akka/actor/Actor.html)
- [Actor References, Paths and Addresses](http://doc.akka.io/docs/akka/snapshot/general/addressing.html)
- [Scalability of Fork Join Pool](http://letitcrash.com/post/17607272336/scalability-of-fork-join-pool)
- [Dispatchers](http://doc.akka.io/docs/akka/snapshot/scala/dispatchers.html) The key concept to understand underlying Akka implementation
- [scala.concurrent.forkjoin.ForkJoinPool.scan() taking up lots of CPU cycles](https://groups.google.com/forum/#!topic/akka-user/6HKTvw4yBnU)
- [Mailboxes](http://doc.akka.io/docs/akka/snapshot/scala/mailboxes.html) See Special Semantics of system.actorOf
- [Akka bounded mailbox implementation using LMAX Disruptor](https://github.com/yngui/akka-disruptor)
- [Durable Mailboxes](http://doc.akka.io/docs/akka/2.0/modules/durable-mailbox.html)
- [Ask: Send-And-Receive-Future](http://doc.akka.io/docs/akka/snapshot/scala/actors.html#ask-send-and-receive-future)
- [Distributed workers with Akka and Scala](http://typesafe.com/activator/template/akka-distributed-workers)
- [Distributed workers with Akka and Java](http://typesafe.com/activator/template/akka-distributed-workers-java)
- [Typesafe Reactive Platform / Akka](http://typesafe.com/platform/runtime/akka)
- [Akka Microkernel](http://doc.akka.io/docs/akka/2.2.3/scala/microkernel.html)
- [Distributed Publish Subscribe in Akka Cluster](http://doc.akka.io/docs/akka/2.2.3/contrib/distributed-pub-sub.html)
- [Introduction to Akka I/O](http://hseeberger.github.io/blog/2013/06/17/introduction-to-akka-i-slash-o/)
- [The akka-camel module allows Untyped Actors to receive and send messages over a great variety of protocols and APIs](http://doc.akka.io/docs/akka/snapshot/scala/camel.html)
- [Agents](http://doc.akka.io/docs/akka/2.1.0/scala/agents.html)
- [Akka and the Java Memory Model](http://doc.akka.io/docs/akka/snapshot/general/jmm.html)
- [Non-blocking Message Flow with Akka Actors](http://www.typesafe.com/blog/non-blocking-message-flow-with-akka-actors)
- [How can akka actor interact between threads](http://stackoverflow.com/questions/9541507/how-can-akka-actor-interact-between-threads)
- [How java thread are heavy weight than scala / akka actors](http://stackoverflow.com/questions/15553857/how-java-thread-are-heavy-weight-than-scala-akka-actors)
- [Typed Actors](http://doc.akka.io/docs/akka/snapshot/scala/typed-actors.html)

### Java

- [From Imperative Programming to Fork/Join to Parallel Streams in Java 8](http://www.infoq.com/articles/forkjoin-to-parallel-streams)
- [Class ForkJoinPool](http://docs.oracle.com/javase/7/docs/api/java/util/concurrent/ForkJoinPool.html)
- [Java concurrency (multi-threading) - Tutorial](http://www.vogella.com/tutorials/JavaConcurrency/article.html)
- [Threads pools with the Executor Framework](http://www.vogella.com/tutorials/JavaConcurrency/article.html#threadpools)
- [Fork-Join in Java 7](http://www.vogella.com/tutorials/JavaConcurrency/article.html#forkjoin)
- [ExecutorService](http://tutorials.jenkov.com/java-util-concurrent/executorservice.html)
- [ForkJoinPool: the Other ExecutorService](http://blog.jessitron.com/2014/02/forkjoinpool-other-executorservice.html)
- [Comparision of different concurrency models: Actors, CSP, Disruptor and Threads](http://java-is-the-new-c.blogspot.com.ar/2014/01/comparision-of-different-concurrency.html)
- [Blocking Spring ThreadPoolTaskExecutor](http://www.jroller.com/ndpar/entry/taskexecutor_blocking_queue)

### .NET

- BlockingCollection Overview http://msdn.microsoft.com/en-us/library/dd997371(v=vs.100).aspx
- BlockingCollection<T> Class http://msdn.microsoft.com/en-us/library/dd267312(v=vs.100).aspx
- AutoResetEvent Class http://msdn.microsoft.com/en-us/library/system.threading.autoresetevent(v=vs.100).aspx
- [TPL and Traditional .NET Framework Asynchronous Programming](http://msdn.microsoft.com/en-us/library/dd997423.aspx)
- [How to: Chain Multiple Tasks with Continuations](http://msdn.microsoft.com/en-us/library/dd537612) also http://msdn.microsoft.com/en-us/library/dd537612(v=vs.100).aspx
- [Task Class](http://msdn.microsoft.com/en-us/library/system.threading.tasks.task)
- [is there a “try to lock, skip if timed out” operation in C#?](http://stackoverflow.com/questions/8546/is-there-a-try-to-lock-skip-if-timed-out-operation-in-c)
- [C#, Can I check on a lock without trying to acquire it?](http://stackoverflow.com/questions/4974729/c-can-i-check-on-a-lock-without-trying-to-acquire-it)

### Hardware

- [Multi-core processor](http://en.wikipedia.org/wiki/Multi-core_processor)

