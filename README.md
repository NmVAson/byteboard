Task List

You may do these tasks in any order, but take note that they are listed in
the order your team has prioritized completing them.

You are NOT expected to complete all tasks. You are expected to write clean,
readable code. You can add comments explaining what you are working on if you
run out of time in the middle of a task.

1. Implement the static GetTotalDistance method in Vehicle.cs.

    This method should return the total distance traversed by the given
    forklift. You can assume the input list is sorted with the earliest ping at
    the start. Once you are done, use Vehicle.GetTotalDistance in your
    implementation of:

          Vehicle.GetAverageSpeed

    GetAverageSpeed should return the average speed (total distance / total
    time) observed for the given forklift. A forklift which has never moved
    would have an average speed of 0 meters per second. You can assume the input
    list is sorted with the earliest ping at the start.
   
    *Assumptions*
    - Distance is in 2D space on a coordinate plane, not lat/lon
    - Ping.Timestamp is a datetime in milliseconds
    - GetAverageSpeed units are in dist/milliseconds

    *TODOs*
    - [X] Refactor GetTotalDistance to use an accumulator rather than having to keep appending the `totalDistance`
    - [ ] Add tests for randomly generated numbers 
    - [ ] Add tests for weird numbers (irrational? imaginary?)

2. Implement the WarehouseServer.GetMostTraveledSince method in
    WarehouseServer.cs.

    This method returns an array of the maxResults forklifts that have
    traveled the most distance since timestamp (inclusive), sorted by those that
    have moved most to least. You can assume the lists of pings for each vehicle
    are sorted with the earliest ping at the start.

   *Assumptions*
    - If distances tie, then sort by vehicle name

   *TODOs*
    - [X] Warehouse test suite has list of action items, failing tests indicate next steps
    - [X] Resolve index out of bounds exception
    - [X] Should add a test for the zero vehicle case
    - [X] Should add a test for the zero ping case
    - [X] Should add a test in case distances tie
    - [X] _Definitely_ can be refactored to reduce the amount of looping 😬
    - [X] Part of refactor should be not to access the pings directly from the Server class, that should be encapsulated into a method named `GetPingsUntil`

3. We want to be as proactive as possible in providing maintenance and repairs
    to our forklifts, especially those which may have been damaged. Implement
    the WarehouseServer.CheckForDamage method in WarehouseServer.cs.

    This method should identify a list of forklifts that might need to be
    inspected. Examples of behavior that might warrant an inspection include
    forklifts which have been driven aggressively (quick acceleration and
    deceleration) or when forklifts collide with one another. You can use any
    heuristics you like, but are encouraged to make sure your decisions are well
    documented and your code is appropriately decomposed.

   *TODOS*  
    Oops! Didn't have enough time for this one, so I'll attempt a quick note on how I would have solved it. 
    - [X] I'd start with an arbitrary definition of "aggressive", parameterized so that it's easy to change once that threshold is defined
    - [ ] I might use the average speed method on each vehicle, and check for deviations from that average
    - [X] I'd likely add a `GetMaxAcceleration` method onto the Vehicle class // a = 2 * (Δd - v_i * Δt) / Δt²
    - [X] Use `GetMaxAcceleration` to retrieve concerning vehicles for that reason
    - [X] For collision points, I'd probably aggregate all of the Pings to see which ones match in X,Y, and timestamp variables, adding in a little st dev.

General Refactoring
- [ ] Use Ping.SecondsBetween