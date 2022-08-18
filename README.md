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
    - Ping.Timestamp is a datetime in milliseconds
    - GetAverageSpeed units are in dist/milliseconds

    *TODOs*
    - [X] Refactor GetTotalDistance to use an accumulator rather than having to keep appending the `totalDistance`
    - [ ] Add tests for randomly generated numbers 
    - [ ] Add tests for weird numbers (irrational? imaginary?)

    *Stuff I Got Wrong*
   - Ping.Timestamp is in seconds (from comment above Ping.SecondsBetween)
   - Position.GetDistance exists
   - Ping.SecondsBetween exists
   - The extra tests in the TODOs are probably overkill
   - Forgot to document the first assumption (distance is not in lat/lon)
   - CalculateDistance belongs on Ping, not Vehicle (similarly to the SecondsBetween method provided) 
   - Unnecessary copying of data into a queue, can use LINQ's Zip 
     - since I'm zipping an array with itself, I want to zip to itself but with an offset of 1 so each element is being aggregated with it's next ping
     - Queue might be more readable and can totally be convinced to revert

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

    *Stuff I Got Wrong*
   - Tests were testing timestamp as an upper bound, not lower bound
   - Implementation missed the "Inclusive" AC on the lower bound
   - Index Out of Bounds 
     - GetRange requires index and count to be a valid range, and was throwing if the list was less than the threshold
     - Take was what I really wanted there 
   - I don't think I needed to implement a tiebreaker (YAGNI!)
   - Sort order was ascending, not descending

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

   *Stuff I Got Wrong*
   - I implemented the acceleration per instructions, but I bet tracking velocity would have more reuse of the methods that are already there, and would be just as good to determine rough drivers
   - I'd want someone to double check my work on calculating acceleration - I think the initial velocity might have to change for each set of points compared 
   - Undocumented assumption that if there aren't enough pings to calculate, just return 0 as in the distance requirements. 
